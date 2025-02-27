import Styles from "@/constants/Styles";
import { ThemedView } from "./ThemedView";
import { ThemedInput } from "./ThemedInput";
import { ThemedText } from "./ThemedText";
import { useState } from "react";
import { useGlobalContext } from "./ContextProvider";
import { Pressable, useColorScheme } from "react-native";
import { useThemeColor } from "@/hooks/useThemeColor";
import { Colors } from "@/constants/Colors";

export default function CreateHero(){
    const [name, setName] = useState("");
    const { api, user } = useGlobalContext();
    const colorScheme = useColorScheme();
    const color = Colors[colorScheme ?? "light"].tint;

    return <ThemedView style={Styles.container}>
        <ThemedText>Wellcome, Hero!</ThemedText>
        <ThemedText>What is your name?</ThemedText>
        <ThemedInput style={{width:'70%'}} value={name} onChangeText={setName} placeholder="..." placeholderTextColor="gray"/>
        <Pressable
            onPress={() =>
              api.CreateHero(name, user)
            }
            style={[Styles.pressable, {width: "70%", borderColor: color}]}
          >
            <ThemedText>Create</ThemedText>
          </Pressable>
    </ThemedView>
}