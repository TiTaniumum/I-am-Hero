import { StyleSheet } from "react-native";

const Styles = StyleSheet.create({
  link: {
    color: "#156beb",
    textDecorationLine: "underline",
  },
  pressable: {
    padding: 5,
    borderRadius: 10,
    width: "auto",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  pressablePressed:{
    backgroundColor: "white",
    
  },
  pressableNotPressed:{
    transitionDuration: '0.5s',
  },
  container: {
    height: "100%",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  header: {
    width: '100%',
    top: 0,
    left: 0,
    right: 0,
    height: 50,
    zIndex: 1000,
    display: "flex",
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center'
  },
  headerheight: {
    height: 50,
    width: '100%',
  },
  headerButtons:{
    padding: 5,
    paddingHorizontal:20,
    width: "auto",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    height: "100%", 
    borderRadius: 0
  },
  row: {
    display: 'flex',
    flexDirection: 'row',
    gap: 10
  }
});

export default Styles;
