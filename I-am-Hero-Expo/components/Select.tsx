import { FlatList, Modal, Pressable, StyleSheet, ViewStyle } from "react-native";
import { ThemedView } from "./ThemedView";
import { ThemedText } from "./ThemedText";
import { useState } from "react";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";

export type SelectProps = {
  selectedValue?: any;
  data: any[];
  getTitle: (item: any) => string;
  onSelect: (item: any) => void;
  style?: ViewStyle
};

export default function Select({
  selectedValue,
  data,
  getTitle,
  onSelect,
  style,
}: SelectProps) {
  const [selected, setSelected] = useState(selectedValue);
  const [modalVisible, setModalVisible] = useState(false);
  const [options, setOptions] = useState(data);

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  return (
    <ThemedView style={[style]}>
      <Pressable
        style={[Styles.pressable, { borderColor: color }]}
        onPress={() => setModalVisible(true)}
      >
        <ThemedText>
          {selected === undefined || selected === null
            ? "..."
            : getTitle(selected)}
        </ThemedText>
      </Pressable>

      <Modal visible={modalVisible} transparent animationType="fade" presentationStyle="overFullScreen">
        <Pressable
          style={styles.overlay}
          onPress={() => setModalVisible(false)}
        >
          <ThemedView style={styles.dropdown}>
            <FlatList
              data={options}
              ItemSeparatorComponent={() => (
                <ThemedView style={styles.separator}></ThemedView>
              )}
              renderItem={({index, item }) => (
                <Pressable
                  key={index}
                  style={styles.option}
                  onPress={() => {
                    setSelected(item);
                    setModalVisible(false);
                    onSelect(item);
                  }}
                >
                  <ThemedText>{getTitle(item)}</ThemedText>
                </Pressable>
              )}
            />
          </ThemedView>
        </Pressable>
      </Modal>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  overlay: {
    top: 0,
    left: 0,
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "rgba(0,0,0,0.3)",
  },
  dropdown: { width: 200, padding: 10, borderRadius: 5 },
  option: { padding: 10 },
  separator: {
    width: "100%",
    borderBottomWidth: 1,
  },
});
