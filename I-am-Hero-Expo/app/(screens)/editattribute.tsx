import { useGlobalContext } from "@/components/ContextProvider";
import Select from "@/components/Select";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { AttributeState, IAttributeDTO } from "@/models/Attribute";
import { FontAwesome, MaterialIcons } from "@expo/vector-icons";
import { useEffect, useState } from "react";
import { FlatList, Pressable, StyleSheet } from "react-native";
import Animated, {
  FlipInXUp,
  FlipOutXDown,
  ZoomOut,
} from "react-native-reanimated";

export default function EditAttributeScreen() {
  const { api, alert, editAttribute, user, setAlpha } = useGlobalContext();
  const [name, setName] = useState("");
  const [description, setdescription] = useState("");
  const [attributeType, setAttributeType] = useState(0);
  const [initialStates, setInitialStates] = useState<AttributeState[]>([]);
  const [states, setStates] = useState<AttributeState[]>([]);
  const [deleteStates, setDeleteStates] = useState<AttributeState[]>([]);
  const [newStates, setNewStates] = useState<AttributeState[]>([]);
  const [state, setState] = useState("");
  const [currentState, setCurrentState] = useState<AttributeState | null>(null);
  const [min, setMin] = useState("");
  const [cur, setCur] = useState("");
  const [max, setMax] = useState("");
  const [availableStates, setAvailableStates] = useState<AttributeState[]>([]);

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  useEffect(() => {
    setAvailableStates([
      ...initialStates.filter(
        (x) => deleteStates.find((y) => y.id == x.id) == undefined
      ),
    ]);
  }, [deleteStates]);

  useEffect(() => {
    setName(editAttribute?.name ?? "");
    setdescription(editAttribute?.description ?? "");
    setAttributeType(editAttribute?.cAttributeTypeId ?? 0);
    const states = user.attributeStates.filter(
      (x) => x.heroAttributeId == editAttribute?.id
    );
    setInitialStates([...states]);
    setStates([...states]);
    const currentState =
      states.find((x) => x.id == editAttribute?.currentStateId) ?? null;
    setCurrentState(currentState);
    setMin(`${editAttribute?.minValue}`);
    setCur(`${editAttribute?.value}`);
    setMax(`${editAttribute?.maxValue}`);
    setAvailableStates([...states]);
  }, []);

  function AddState() {
    if (state == "") return;
    if (states.find((x) => x.name == state) != undefined) {
      setState("");
      return;
    }
    setNewStates([
      ...newStates,
      new AttributeState({
        id: -1,
        heroAttributeId: editAttribute?.id ?? -1,
        name: state,
      }),
    ]);
    setStates([
      ...states,
      new AttributeState({
        id: -1,
        heroAttributeId: editAttribute?.id ?? -1,
        name: state,
      }),
    ]);
    setState("");
  }

  function DeleteState(i: number) {
    const deletionState = states.splice(i, 1);
    setStates([...states]);
    if (initialStates.find((x) => x.id == deletionState[0].id) != undefined)
      setDeleteStates([...deleteStates, ...deletionState]);
    const index = newStates.findIndex((x) => x.name === deletionState[0].name);
    if (index != -1) {
      newStates.splice(index, 1);
      setNewStates([...newStates]);
    }
  }

  function Save() {
    let minValue: number = +min;
    let curValue: number = +cur;
    let maxValue: number = +max;
    if (minValue > maxValue) {
      alert(Resource.get("warning!"), Resource.get("minbigmax"));
      setMin(max);
      return;
    }
    if (curValue > maxValue) {
      alert(Resource.get("warning!"), Resource.get("curbigmax"));
      setCur(max);
      return;
    }
    if (curValue < minValue) {
      alert(Resource.get("warning!"), Resource.get("curlesmin"));
      setCur(min);
      return;
    }
    const attributeStates = newStates.map((item) => {
      return { heroAttributeId: item.heroAttributeId, name: item.name };
    });
    api
      .CreateAttributeStates(attributeStates)
      .catch((error) =>
        alert(Resource.get("error!"), Resource.get("somethingwrong"))
      );
    const ids: number[] = deleteStates.map((x) => x.id);
    api.DeleteAttributeStates(ids);
    const attribute: IAttributeDTO = {
      id: editAttribute?.id,
      name,
      description,
      cAttributeTypeId: attributeType,
      minValue: minValue,
      value: curValue,
      maxValue: maxValue,
      currentStateId: currentState?.id || null,
    };
    api
      .EditAttribute(attribute)
      .then(() => {
        alert(Resource.get("saved!"));
      })
      .catch((error) => {
        alert(Resource.get("error!"), Resource.get("somethingwrong"));
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
        placeholder={Resource.get("attributename")}
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedInput
        value={description}
        onChangeText={setdescription}
        placeholder={Resource.get("description")}
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedView style={styles.container}>
        <Pressable
          style={({ pressed, hovered }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.switchButton,
            { borderColor: attributeType == 1 ? color : "gray" },
          ]}
          onPress={() => {
            setAttributeType(1);
          }}
        >
          <ThemedText style={{ color: attributeType == 1 ? color : "gray" }}>
            {Resource.get("numeric")}
          </ThemedText>
        </Pressable>
        <Pressable
          style={({ hovered, pressed }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.switchButton,
            { borderColor: attributeType == 2 ? color : "gray" },
          ]}
          onPress={() => {
            setAttributeType(2);
          }}
        >
          <ThemedText style={{ color: attributeType == 2 ? color : "gray" }}>
            {Resource.get("state")}
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
            placeholder={Resource.get("min")}
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={min}
            onChangeText={setMin}
          />
          <ThemedInput
            placeholder={Resource.get("cur")}
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={cur}
            onChangeText={setCur}
          />
          <ThemedInput
            placeholder={Resource.get("max")}
            placeholderTextColor="gray"
            keyboardType="numeric"
            style={styles.numericInput}
            value={max}
            onChangeText={setMax}
          />
        </Animated.View>
      )}
      {attributeType == 2 && (
        <>
          <Animated.View
            entering={FlipInXUp}
            exiting={FlipOutXDown}
            style={[styles.container, { flexDirection: "column", gap: 10 }]}
          >
            <ThemedView style={styles.currentState}>
              <ThemedText style={{ width: "30%" }}>
                {Resource.get("curstate")}
              </ThemedText>
              <Select
                selectedValue={currentState}
                data={[
                  ...initialStates.filter(
                    (x) => deleteStates.find((y) => y.id == x.id) == undefined
                  ),
                ]}
                getTitle={(item) => item.name}
                displayTitle={(item) => item.name}
                onSelect={setCurrentState}
                style={{ width: "70%" }}
              />
            </ThemedView>
            <ThemedView style={{ flexDirection: "row", width: "100%" }}>
              <ThemedInput
                placeholder={Resource.get("statename")}
                placeholderTextColor="gray"
                style={{ width: "80%" }}
                value={state}
                onChangeText={setState}
              />
              <Pressable
                style={({ pressed, hovered }) => [
                  hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
                  pressed
                    ? { backgroundColor: color }
                    : { transitionDuration: "0.2s" },
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
                    <ThemedText style={{ width: "90%" }}>
                      {item.name}
                    </ThemedText>
                    <Pressable
                      style={({ pressed, hovered }) => [
                        hovered
                          ? { backgroundColor: setAlpha(color, 0.5) }
                          : {},
                        pressed
                          ? { backgroundColor: color }
                          : { transitionDuration: "0.2s" },
                        Styles.pressable,
                        { borderColor: color },
                      ]}
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
        </>
      )}
      {name != "" && attributeType != 0 && (
        <Pressable
          style={({ pressed, hovered }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.switchButton,
            { borderColor: color },
          ]}
          onPress={Save}
        >
          <ThemedText>{Resource.get("save")}</ThemedText>
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
  currentState: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
  },
});
