import React, {
  createContext,
  useState,
  useContext,
  ReactNode,
  useEffect,
  useRef,
} from "react";
import { Platform } from "react-native";
import AsyncStorage from '@react-native-async-storage/async-storage';

const storeData = async (key: string, value: any) => {
  try {
    await AsyncStorage.setItem(key, JSON.stringify(value));
  } catch (error) {
    console.error('Error storing data:', error);
  }
};

const getData = async (key: string) => {
  try {
    const value = await AsyncStorage.getItem(key);
    return value ? JSON.parse(value) : null;
  } catch (error) {
    console.error('Error retrieving data:', error);
  }
};
function getDelay(timestamp: number) {
  let time = new Date(timestamp);
  let now = new Date();
  let timeEx = (time.getHours() * 60 + time.getMinutes()) * 60000;
  let timeNow = (now.getHours() * 60 + now.getMinutes()) * 60000;
  return timeEx - timeNow;
}

type GlobalContext = {
  token: string | null;
  SetNewToken: (value:string) => void
};

const Context = createContext<GlobalContext>({} as GlobalContext);

export function ContextProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(null);
  async function SetNewToken(t: string){
    if(t === '') return
    await AsyncStorage.setItem("authToken", t);
    setToken(t);
  }
  useEffect(() => {
    async function getToken(){
      const value = await AsyncStorage.getItem("authToken");
      console.log(value);
      setToken(value);
    }
    getToken();
  }, []);
  return (
    <Context.Provider
      value={{
        token,
        SetNewToken
      }}
    >
      {children}
    </Context.Provider>
  );
}

export function useGlobalContext() {
  const context = useContext(Context);
  if (context === undefined) {
    throw new Error("useGlobalContext должен использовать ContextProvider");
  }
  return context;
}
