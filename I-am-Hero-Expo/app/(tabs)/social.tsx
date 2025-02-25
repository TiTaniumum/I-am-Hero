import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";

export default function SocialScreen() {
  const {isToken} = useGlobalContext();
  if(!isToken)
      return <Auth/>
  return (
    <ThemedView style={{height: '100%', display: 'flex', justifyContent: 'center', alignItems: "center"}}>
      <ThemedText>Not implemented</ThemedText>
    </ThemedView>
  );
}
