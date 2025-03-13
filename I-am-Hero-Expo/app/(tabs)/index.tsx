import { Image, StyleSheet, Platform } from "react-native";

import { HelloWave } from "@/components/HelloWave";
import ParallaxScrollView from "@/components/ParallaxScrollView";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { useGlobalContext } from "@/components/ContextProvider";
import Auth from "@/components/Auth";
import CreateHero from "@/components/CreateHero";
import Styles from "@/constants/Styles";

export default function HomeScreen() {
  const { isToken, isHero } = useGlobalContext();
  if (!isToken) return <Auth />;
  if (!isHero) return <CreateHero />;
  return (
    <ThemedView style={Styles.container}>
      <ThemedText>Main Screen</ThemedText>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  
});
