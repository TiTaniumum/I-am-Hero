import { Alert, Pressable, StyleSheet, TextInput } from "react-native";
import { ThemedText } from "./ThemedText";
import { ThemedView } from "./ThemedView";
import { ThemedInput } from "./ThemedInput";
import { useState } from "react";
import axios from "axios";
import { useGlobalContext } from "./ContextProvider";

export default function Auth() {
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordRepeat, setPasswordRepeat] = useState("");

  const { SetNewToken } = useGlobalContext();

  function login() {
    axios
      .post("http://192.168.1.65:8080/api/Auth/login", {
        Email: email,
        Password: password,
        ApplicationId: 3,
      })
      .then(function (response) {
        SetNewToken(response.data);
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  }
  function register() {
    if (password == passwordRepeat)
      axios
        .post(`http://192.168.1.65:8080/api/Auth/register`, {
          email: email,
          password: password,
        })
        .then(function (response) {
          Alert.alert("success");
          setIsLogin(true);
        })
        .catch(function (error) {
          console.log(error);
        });
    else Alert.alert("passwords have to be the same");
  }
  return (
    <>
      {isLogin && (
        <ThemedView style={styles.container}>
          <ThemedText>You are not authorized</ThemedText>
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
          <Pressable onPress={login} style={styles.button}>
            <ThemedText>Log in</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>Don't have an account? </ThemedText>
            <Pressable
              onPress={() => {
                setIsLogin(false);
              }}
            >
              <ThemedText
                style={{ color: "#156beb", textDecorationLine: "underline" }}
              >
                Register
              </ThemedText>
            </Pressable>
          </ThemedView>
        </ThemedView>
      )}
      {!isLogin && (
        <ThemedView style={styles.container}>
          <ThemedText>You are not authorized</ThemedText>
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
          <Pressable onPress={register} style={styles.button}>
            <ThemedText>Register</ThemedText>
          </Pressable>
          <ThemedView style={{ display: "flex", flexDirection: "row" }}>
            <ThemedText>Already have an account? </ThemedText>
            <Pressable
              onPress={() => {
                setIsLogin(true);
              }}
            >
              <ThemedText
                style={{ color: "#156beb", textDecorationLine: "underline" }}
              >
                Log in
              </ThemedText>
            </Pressable>
          </ThemedView>
        </ThemedView>
      )}
    </>
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
  button: {
    padding: 5,
    borderColor: "white",
    borderWidth: 1,
    borderRadius: 10,
    width: "70%",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  input: {
    width: "70%",
  },
});
