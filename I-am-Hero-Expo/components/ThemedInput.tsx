import { StyleSheet, TextInput, TextInputProps, View, type ViewProps } from 'react-native';

import { useThemeColor } from '@/hooks/useThemeColor';

export type ThemedInputProps = TextInputProps & {
  lightColor?: string;
  darkColor?: string;
};

export function ThemedInput({ style, lightColor, darkColor, ...otherProps }: ThemedInputProps) {
  const backgroundColor = useThemeColor({ light: lightColor, dark: darkColor }, 'background');
  const color = useThemeColor({ light: lightColor, dark: darkColor }, 'text');
  return <TextInput style={[{ backgroundColor }, {color}, style, styles.input]} {...otherProps} />;
}

const styles = StyleSheet.create({
    input:{
        borderColor: 'white',
        borderWidth: 1,
        borderRadius: 10,
        padding: 5
    }
});
