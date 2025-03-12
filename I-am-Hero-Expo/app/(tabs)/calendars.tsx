import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import CreateHero from "@/components/CreateHero";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";

export default function CalendarScreen() {
  const { isToken, isHero } = useGlobalContext();
  if (!isToken) return <Auth />;
  if (!isHero) return <CreateHero />;
  return (
    <ThemedView style={Styles.container}>
      <ThemedText>Daylies</ThemedText>
    </ThemedView>
  );
}
