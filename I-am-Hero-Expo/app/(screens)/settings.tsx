import { useGlobalContext } from "@/components/ContextProvider";
import Select from "@/components/Select";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { router } from "expo-router";
import { useEffect, useState } from "react";
import { Pressable } from "react-native";

export default function SettingsScreen() {
  const { api, settings, loc, setLoc, setAlpha } = useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  useEffect(() => {
    settings.GetCurrentLocalization().then((currentLocalization) => {
      setLoc(currentLocalization);
    });
  }, []);

  function OnSelectLocalizationHandle(item: string) {
    setLoc(item);
    settings.UpdateLocalization(item);
  }

  return (
    <ThemedView style={Styles.container}>
      <Select
        selectedValue={loc}
        data={Resource.localizations}
        getTitle={(item) => `${Resource.get(item)}`}
        onSelect={OnSelectLocalizationHandle}
        style={{ width: "70%" }}
        displayTitle={(item) =>
          `${Resource.get("language")}: ${Resource.get(item)}`
        }
      />
      <Pressable
        onPress={() => {
          api.Logout();
          router.back();
        }}
        style={({ pressed, hovered }) => [
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          pressed ? { backgroundColor: color } : { transitionDuration: "0.2s" },
          Styles.pressable,
          { width: "70%", borderColor: color },
        ]}
      >
        <ThemedText>{Resource.get("logout")}</ThemedText>
      </Pressable>
    </ThemedView>
  );
}
