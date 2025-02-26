import { Pressable, StyleSheet } from "react-native";
import { ThemedText } from "./ThemedText";
import { ThemedView } from "./ThemedView";
import { ThemedInput } from "./ThemedInput";
import { useState } from "react";
import { useGlobalContext } from "./ContextProvider";
import Styles from "@/constants/Styles";
import Resource from "@/constants/Resource";

export default function Auth() {
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordRepeat, setPasswordRepeat] = useState("");

  const { api } = useGlobalContext();

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
            style={[Styles.pressable, {width: "70%"}]}
          >
            <ThemedText>{Resource.get("login")}</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>{Resource.get("noAccount")}{" "}</ThemedText>
            <Pressable
              onPress={() => {
                setIsLogin(false);
              }}
            >
              <ThemedText style={Styles.link}>{Resource.get("register")}</ThemedText>
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
            style={[Styles.pressable, {width: "70%"}]}
          >
            <ThemedText>{Resource.get("register")}</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>{Resource.get("alreadyAccount")}{" "}</ThemedText>
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
