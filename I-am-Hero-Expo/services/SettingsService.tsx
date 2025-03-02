import Resource from "@/constants/Resource";
import AsyncStorage from "@react-native-async-storage/async-storage";

export default class SettingsService{
    async UpdateLocalization(loc?: string){
        if(loc){
            Resource.loc = loc;
            AsyncStorage.setItem("loc", loc);
            return;
        }
        const _loc: string | null = await AsyncStorage.getItem("loc");
        if(_loc)
            Resource.loc = _loc;
    }
}