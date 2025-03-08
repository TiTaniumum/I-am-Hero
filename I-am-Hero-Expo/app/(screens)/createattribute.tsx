import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { IAttributeDTO } from "@/models/Attribute";
import { FontAwesome, MaterialIcons } from "@expo/vector-icons";
import { useState } from "react";
import { FlatList, Pressable, StyleSheet } from "react-native";
import Animated, {
  FadeIn,
  FlipInXUp,
  FlipOutXDown,
  ZoomOut,
} from "react-native-reanimated";

export default function CreateAttributeScreen() {
  const { api, alert } = useGlobalContext();
  const [name, setName] = useState("");
  const [description, setdescription] = useState("");
  const [attributeType, setAttributeType] = useState(0);
  const [states, setStates] = useState<string[]>([]);
  const [state, setState] = useState("");
  const [min, setMin] = useState("");
  const [cur, setCur] = useState("");
  const [max, setMax] = useState("");

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  function AddState() {
    if (state == "") return;
    if (states.indexOf(state) != -1) {
      setState("");
      return;
    }
    setStates([...states, state]);
    setState("");
  }

  function DeleteState(i: number) {
    states.splice(i, 1);
    setStates([...states]);
  }

  function Create() {
    let minValue: number = +min;
    let curValue: number = +cur;
    let maxValue: number = +max;
    if (minValue > maxValue) {
      alert("Warning", "Minimum value cannot be bigger than maximum");
      setMin(max);
      return;
    }
    if (curValue > maxValue) {
      alert("Warning", "Current value cannot be bigger than maximum");
      setCur(max);
      return;
    }
    if (curValue < minValue) {
      alert("Warning", "Current value cannot be lesser than minimum");
      setCur(min);
      return;
    }
    const attribute: IAttributeDTO = {
      name,
      description,
      cAttributeTypeID: attributeType,
      minValue: minValue,
      value: curValue,
      maxValue: maxValue,
      currentStateID: null,
    };
    api
      .CreateAttribute(attribute)
      .then((j) => {
        if(states.length == 0) {
            alert("Created!");
            return;
        }
        const attributeStates = states.map((str) => {
          return { heroAttributeId: j.id, name: str };
        });
        api
          .CreateAttributeStates(attributeStates)
          .then(() => {
            alert("Created!");
          })
          .catch((error) => alert("ERROR", "Something went wrong..."));
      })
      .catch((error) => {
        alert("ERROR", "Something went wrong...");
      });
  }
  return (
    <ThemedView
      style={[
        Styles.container,
        { justifyContent: "flex-start", paddingTop: 20, overflow: "scroll" },
      ]}
    >
      <ThemedInput
        value={name}
        onChangeText={setName}
        placeholder="Attribute name..."
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedInput
        value={description}
        onChangeText={setdescription}
        placeholder="Description ..."
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedView style={styles.container}>
        <Pressable
          style={[
            Styles.pressable,
            styles.switchButton,
            { borderColor: attributeType == 1 ? color : "gray" },
          ]}
          onPress={() => {
            setAttributeType(1);
          }}
        >
          <ThemedText style={{ color: attributeType == 1 ? color : "gray" }}>
            Numeric
          </ThemedText>
        </Pressable>
        <Pressable
          style={[
            Styles.pressable,
            styles.switchButton,
            { borderColor: attributeType == 2 ? color : "gray" },
          ]}
          onPress={() => {
            setAttributeType(2);
          }}
        >
          <ThemedText style={{ color: attributeType == 2 ? color : "gray" }}>
            State
          </ThemedText>
        </Pressable>
      </ThemedView>
      {attributeType == 1 && (
        <Animated.View
          entering={FlipInXUp}
          exiting={FlipOutXDown}
          style={styles.container}
        >
          <ThemedInput
            placeholder="min"
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={min}
            onChangeText={setMin}
          />
          <ThemedInput
            placeholder="cur"
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={cur}
            onChangeText={setCur}
          />
          <ThemedInput
            placeholder="max"
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={max}
            onChangeText={setMax}
          />
        </Animated.View>
      )}
      {attributeType == 2 && (
        <Animated.View
          entering={FlipInXUp}
          exiting={FlipOutXDown}
          style={[styles.container, { flexDirection: "column", gap: 10 }]}
        >
          <ThemedView style={{ flexDirection: "row", width: "100%" }}>
            <ThemedInput
              placeholder="state name"
              placeholderTextColor="gray"
              style={{ width: "80%" }}
              value={state}
              onChangeText={setState}
            />
            <Pressable
              style={[
                {
                  borderColor: color,
                  width: "20%",
                  justifyContent: "center",
                  alignItems: "center",
                },
              ]}
              onPress={AddState}
            >
              <FontAwesome name="plus-square-o" size={30} color={color} />
            </Pressable>
          </ThemedView>
          <FlatList
            data={states}
            renderItem={({ index, item }) => (
              <Animated.View entering={FlipInXUp} exiting={ZoomOut}>
                <ThemedView
                  style={{
                    flexDirection: "row",
                    justifyContent: "space-between",
                  }}
                >
                  <ThemedText style={{ width: "90%" }}>{item}</ThemedText>
                  <Pressable
                    style={[Styles.pressable, { borderColor: color }]}
                    onPress={() => {
                      DeleteState(index);
                    }}
                  >
                    <MaterialIcons name="delete" size={15} color={color} />
                  </Pressable>
                </ThemedView>
              </Animated.View>
            )}
            contentContainerStyle={styles.statesContainer}
          />
        </Animated.View>
      )}
      {name != "" && attributeType != 0 && (
        <Pressable
          style={[
            Styles.pressable,
            styles.switchButton,
            { borderColor: color },
          ]}
          onPress={Create}
        >
          <ThemedText>Create</ThemedText>
        </Pressable>
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: "space-between",
    width: "87%",
  },
  input: {
    width: "87%",
  },
  switchButton: {
    width: "40%",
  },
  numericInput: {
    width: "30%",
  },
  statesContainer: {
    width: "100%",
    minHeight: 50,
    borderColor: "gray",
    borderWidth: 2,
    borderRadius: 10,
    borderStyle: "dashed",
    paddingHorizontal: 20,
    overflow: "hidden",
    paddingVertical: 5,
  },
});
