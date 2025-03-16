import Resource from "@/constants/Resource";
import { Stack } from "expo-router";

export default function Layout() {
  return (
    <Stack>
      <Stack.Screen name="achievements" options={{ title: Resource.get("achievements") }} />
      <Stack.Screen name="attributes" options={{ title: Resource.get("attributes") }} />
      <Stack.Screen name="biography" options={{ title: Resource.get("diary") }} />
      <Stack.Screen name="createattribute" options={{ title: Resource.get("createattribute") }} />
      <Stack.Screen name="createbiography" options={{ title: Resource.get("createbiography") }} />
      <Stack.Screen name="createquest" options={{ title: Resource.get("createquest") }} />
      <Stack.Screen name="createskill" options={{ title: Resource.get("createskill") }} />
      <Stack.Screen name="editattribute" options={{ title: Resource.get("editattribute") }} />
      <Stack.Screen name="editbiography" options={{ title: Resource.get("editbiography") }} />
      <Stack.Screen name="editquest" options={{ title: Resource.get("editquest") }} />
      <Stack.Screen name="editskill" options={{ title: Resource.get("editskill") }} />
      <Stack.Screen name="habbits" options={{ title: Resource.get("habbits") }} />
      <Stack.Screen name="settings" options={{ title: Resource.get("settings") }} />
      <Stack.Screen name="skills" options={{ title: Resource.get("skills") }} />
      <Stack.Screen name="statuseffects" options={{ title: Resource.get("statuseffects") }} />
    </Stack>
  );
}
