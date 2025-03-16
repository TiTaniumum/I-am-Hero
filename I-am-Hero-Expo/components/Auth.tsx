import { Pressable, StyleSheet } from "react-native";
import { ThemedText } from "./ThemedText";
import { ThemedView } from "./ThemedView";
import { ThemedInput } from "./ThemedInput";
import { useEffect, useState } from "react";
import { useGlobalContext } from "./ContextProvider";
import Styles from "@/constants/Styles";
import Resource from "@/constants/Resource";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";
import Select from "./Select";

export default function Auth() {
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordRepeat, setPasswordRepeat] = useState("");

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  const { api, settings, loc, setLoc, setAlpha } = useGlobalContext();

  function OnSelectLocalizationHandle(item: string) {
    setLoc(item);
    settings.UpdateLocalization(item);
  }

  return (
    <ThemedView style={styles.container}>
      {isLogin && (
        <>
          <ThemedText>{Resource.get("notAuth")}</ThemedText>
          <ThemedInput
            value={email}
            onChangeText={setEmail}
            style={styles.input}
            placeholder="user@gmail.com..."
            placeholderTextColor="gray"
          />
          <ThemedInput
            value={password}
            onChangeText={setPassword}
            style={styles.input}
            placeholder="Qwerty12345..."
            placeholderTextColor="gray"
          />
          <Pressable
            onPress={() => api.Login(email, password)}
            style={({ pressed, hovered }) => [
              hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
              pressed
                ? { backgroundColor: color }
                : { transitionDuration: "0.2s" },
              Styles.pressable,
              { width: "70%", borderColor: color, borderWidth: 2 },
            ]}
          >
            <ThemedText>{Resource.get("login")}</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>{Resource.get("noAccount")} </ThemedText>
            <Pressable
              onPress={() => {
                setIsLogin(false);
              }}
            >
              <ThemedText style={Styles.link}>
                {Resource.get("register")}
              </ThemedText>
            </Pressable>
          </ThemedView>
        </>
      )}
      {!isLogin && (
        <>
          <ThemedText>{Resource.get("registration")}</ThemedText>
          <ThemedInput
            value={email}
            onChangeText={setEmail}
            style={styles.input}
            placeholder="user@gmail.com..."
            placeholderTextColor="gray"
          />
          <ThemedInput
            value={password}
            onChangeText={setPassword}
            style={styles.input}
            placeholder="Qwerty12345..."
            placeholderTextColor="gray"
          />
          <ThemedInput
            value={passwordRepeat}
            onChangeText={setPasswordRepeat}
            style={styles.input}
            placeholder="Qwerty12345..."
            placeholderTextColor="gray"
          />
          <Pressable
            onPress={() =>
              api.Register(email, password, passwordRepeat, setIsLogin)
            }
            style={({ pressed, hovered }) => [
              hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
              pressed
                ? { backgroundColor: color }
                : { transitionDuration: "0.2s" },
              Styles.pressable,
              { width: "70%", borderColor: color },
            ]}
          >
            <ThemedText>{Resource.get("register")}</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>{Resource.get("alreadyAccount")} </ThemedText>
            <Pressable
              onPress={() => {
                setIsLogin(true);
              }}
            >
              <ThemedText
                style={{ color: "#156beb", textDecorationLine: "underline" }}
              >
                {Resource.get("login")}
              </ThemedText>
            </Pressable>
          </ThemedView>
        </>
      )}
      <Select
        selectedValue={loc}
        data={Resource.localizations}
        getTitle={(item) => `${Resource.get(item)}`}
        displayTitle={(item) =>
          `${Resource.get("language")}: ${Resource.get(item)}`
        }
        onSelect={OnSelectLocalizationHandle}
        style={{ width: "70%" }}
      />
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    gap: 10,
  },
  input: {
    width: "70%",
  },
});
