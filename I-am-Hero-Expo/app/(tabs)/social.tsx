import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import CreateHero from "@/components/CreateHero";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";

export default function SocialScreen() {
  const { isToken, isHero } = useGlobalContext();
  if (!isToken) return <Auth />;
  if (!isHero) return <CreateHero />;
  return (
    <ThemedView
      style={{
        height: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <ThemedText>Not implemented</ThemedText>
    </ThemedView>
  );
}
