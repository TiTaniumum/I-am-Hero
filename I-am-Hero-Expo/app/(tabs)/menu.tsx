import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";

export default function MenuScreen(){
    const {token} = useGlobalContext();
      if(token === null)
        return <Auth/>
    return <></>;
}