import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";

export default function StatusEffectsScreen() {
  return (
    <ThemedView style={Styles.container}>
      <ThemedText>Status effects screen</ThemedText>
    </ThemedView>
  );
}
