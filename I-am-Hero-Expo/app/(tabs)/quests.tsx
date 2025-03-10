import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import CreateHero from "@/components/CreateHero";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import QuestIcon from "@/icons/QuestIcon";
import { useEffect } from "react";
import { useColorScheme } from "react-native";

export default function QuestsScreen() {
  const { isToken, isHero, alert, api } = useGlobalContext();
  if (!isToken) return <Auth />;
  if (!isHero) return <CreateHero />;
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  return (
    <ThemedView style={Styles.container}>
      <QuestIcon color={color} size={200} />
    </ThemedView>
  );
}
