import React, {
  createContext,
  useState,
  useContext,
  ReactNode,
  useEffect,
  useRef,
} from "react";
import AsyncStorage from "@react-native-async-storage/async-storage";
import ApiService from "@/services/ApiService";
import AlertModal, { AlertModalProps } from "./AlertModal";
import SettingsService from "@/services/SettingsService";
import User from "@/models/User";
import { Attribute } from "@/models/Attribute";
import { Portal, Provider } from "react-native-paper";
import { Skill } from "@/models/Skill";

const user = new User();
const api = new ApiService(user);
const settings = new SettingsService();

export interface iAlert {
  title?: string;
  message?: string;
}

type GlobalContext = {
  isToken: boolean;
  api: ApiService;
  alert: (title?: string, message?: string) => void;
  isHero: boolean;
  user: User;
  bioText: string;
  setBioText: (value: string) => void;
  editBioID: number;
  setEditBioID: (value: number) => void;
  editBioText: string;
  setEditBioText: (value: string) => void;
  editAttribute: Attribute | undefined;
  setEditAttribute: (value: Attribute) => void;
  settings: SettingsService;
  loc: string;
  setLoc: (value: string) => void;
  setAlpha: (hex: string, alpha: number) => string;
  eidtSkill: Skill | undefined;
  setEditSkill: (value: Skill) => void;
};

const Context = createContext<GlobalContext>({} as GlobalContext);

export function ContextProvider({ children }: { children: ReactNode }) {
  const [isToken, setIsToken] = useState<boolean>(false);
  const [isAlertVisible, setIsAlertVisible] = useState<boolean>(false);
  const [alertObj, setAlertObj] = useState<iAlert>({ title: "", message: "" });
  const [isHero, setIsHero] = useState<boolean>(false);
  const [bioText, setBioText] = useState("");
  const [editBioID, setEditBioID] = useState<number>(0);
  const [editBioText, setEditBioText] = useState("");
  const [editAttribute, setEditAttribute] = useState<Attribute>();
  const [loc, setLoc] = useState("en");
  const [eidtSkill, setEditSkill] = useState<Skill>()
  function onAlertClose() {
    setIsAlertVisible(false);
  }
  function alert(title?: string, message?: string) {
    setAlertObj({ title, message });
    setIsAlertVisible(true);
  }
  api.setIsToken = setIsToken;
  user.setIsHero = setIsHero;
  useEffect(() => {
    settings.UpdateLocalization().then(()=>{
      settings.GetCurrentLocalization().then((currentLocalization)=>{
        setLoc(currentLocalization);
      })
    })
    api.GetToken();
    api.alert = alert;
    user.Init().then(() => {
      if (!user.hero) api.GetHero(user);
    });
  }, []);

  const setAlpha = (hex: string, alpha: number) => {
    const alphaHex = Math.round(alpha * 255).toString(16).padStart(2, "0");
    return hex + alphaHex;
  };

  return (
    <Context.Provider
      value={{
        isToken,
        api,
        alert,
        isHero,
        user,
        bioText,
        setBioText,
        editBioID,
        setEditBioID,
        editBioText,
        setEditBioText,
        editAttribute,
        setEditAttribute,
        settings,
        loc,
        setLoc,
        setAlpha,
        eidtSkill, 
        setEditSkill,
      }}
    >
      <Provider>
        {children}
        <Portal>
          <AlertModal
            visible={isAlertVisible}
            title={alertObj.title}
            message={alertObj.message}
            onClose={onAlertClose}
          />
        </Portal>
      </Provider>
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
