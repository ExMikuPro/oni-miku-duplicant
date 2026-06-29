using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Klei.AI;
using UnityEngine;

namespace PrintMiku.Mechanics;

internal static class MikuMechanicsRuntime
{
    private const string PersonalityKey = "MIKU";
    private const string DisplayName = "Hatsune Miku";

    private static readonly AttributeLevelSpec[] AttributeSpecs =
    {
        new("Art", 9),
        new("Machinery", 3),
        new("Digging", -2),
        new("Construction", -1),
        new("Strength", -2)
    };

    private static readonly string[] SkillGroupFieldNames = { "Art", "Technicals" };
    private static readonly HashSet<string> WarningKeys = new(StringComparer.Ordinal);

    internal static void TryApplySpawnFallbacks(MinionStartingStats startingStats, GameObject go)
    {
        if (!IsMiku(startingStats))
        {
            return;
        }

        try
        {
            ApplyAttributeLevelFallback(go);
            ApplyAptitudeFallback(go);
        }
        catch (Exception ex)
        {
            WarnOnce("spawn-fallback-failed", $"Failed to apply Miku spawn-time fallbacks: {ex}");
        }
    }

    private static void ApplyAttributeLevelFallback(GameObject go)
    {
        var levels = go.GetComponent<AttributeLevels>();
        if (levels == null)
        {
            WarnOnce("missing-attribute-levels", "AttributeLevels component was missing on the spawned Miku duplicant.");
            return;
        }

        var attributes = Db.Get()?.Attributes;
        if (attributes == null)
        {
            WarnOnce("missing-attributes-db", "Db.Get().Attributes was null while applying Miku fallback levels.");
            return;
        }

        foreach (var spec in AttributeSpecs)
        {
            var attribute = ResolveDbField(attributes, spec.FieldName);
            var attributeId = ResolveId(attribute, spec.FieldName);
            levels.SetLevel(attributeId, spec.Level);
        }
    }

    private static void ApplyAptitudeFallback(GameObject go)
    {
        var resume = go.GetComponent<MinionResume>();
        if (resume == null)
        {
            WarnOnce("missing-minion-resume", "MinionResume component was missing on the spawned Miku duplicant.");
            return;
        }

        var skillGroups = Db.Get()?.SkillGroups;
        if (skillGroups == null)
        {
            WarnOnce("missing-skill-groups-fallback", "Db.Get().SkillGroups was null while applying Miku fallback aptitudes.");
            return;
        }

        foreach (var fieldName in SkillGroupFieldNames)
        {
            var skillGroup = ResolveDbField(skillGroups, fieldName);
            var skillGroupId = ResolveId(skillGroup, fieldName);
            resume.SetAptitude(skillGroupId, 1);
        }
    }

    private static bool IsMiku(MinionStartingStats startingStats)
    {
        if (startingStats == null)
        {
            return false;
        }

        var statsName = startingStats.Name;
        var statsNameKey = startingStats.NameStringKey;
        var personality = ReadMember(startingStats, "personality");

        var personalityType = ReadMemberAsString(personality, "personalityType");
        var personalityName = ReadMemberAsString(personality, "nameStringKey");

        var nameMatch =
            string.Equals(statsName, DisplayName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(statsNameKey, DisplayName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(personalityName, DisplayName, StringComparison.OrdinalIgnoreCase);

        var keyMatch = string.Equals(personalityType, PersonalityKey, StringComparison.OrdinalIgnoreCase);

        return keyMatch || nameMatch;
    }

    private static object? ResolveDbField(object? container, string fieldName)
    {
        if (container == null)
        {
            return null;
        }

        var field = AccessTools.Field(container.GetType(), fieldName);
        return field?.GetValue(container);
    }

    private static object? ReadMember(object? instance, string memberName)
    {
        if (instance == null)
        {
            return null;
        }

        var type = instance.GetType();
        var field = AccessTools.Field(type, memberName);
        if (field != null)
        {
            return field.GetValue(instance);
        }

        var property = AccessTools.Property(type, memberName);
        return property?.GetValue(instance, null);
    }

    private static string? ReadMemberAsString(object? instance, string memberName)
    {
        var value = ReadMember(instance, memberName);
        return value?.ToString();
    }

    private static string ResolveId(object? resource, string fallback)
    {
        return ReadMemberAsString(resource, "Id")
               ?? ReadMemberAsString(resource, "id")
               ?? ReadMemberAsString(resource, "choreGroupID")
               ?? fallback;
    }

    private static void WarnOnce(string key, string message)
    {
        if (!WarningKeys.Add(key))
        {
            return;
        }

        Debug.LogWarning($"[PrintMiku.Mechanics] {message}");
    }

    private struct AttributeLevelSpec
    {
        internal AttributeLevelSpec(string fieldName, int level)
        {
            FieldName = fieldName;
            Level = level;
        }

        internal string FieldName { get; private set; }

        internal int Level { get; private set; }
    }
}

[HarmonyPatch(typeof(MinionStartingStats), nameof(MinionStartingStats.Apply))]
internal static class MinionStartingStats_Apply_Patch
{
    private static void Postfix(MinionStartingStats __instance, GameObject go)
    {
        MikuMechanicsRuntime.TryApplySpawnFallbacks(__instance, go);
    }
}
