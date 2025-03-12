import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { router } from "expo-router";
import { Pressable } from "react-native";

export default function SettingsScreen() {
  const { api } = useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  return (
    <ThemedView style={Styles.container}>
      <Pressable
        onPress={() => {api.Logout(); router.back()}}
        style={[Styles.pressable, { width: "70%", borderColor: color }]}
      >
        <ThemedText>Logout</ThemedText>
      </Pressable>
    </ThemedView>
  );
}
