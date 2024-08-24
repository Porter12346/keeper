import { Keep } from "@/models/Keep.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class KeepsService{
    setActive(keepProp) {
        AppState.activeKeep = keepProp;
    }

    async getKeeps() {
        const response = await api.get('api/keeps')
        const keeps = response.data.map((data)=>new Keep(data))
        AppState.keeps = keeps
    }

}
export const keepsService = new KeepsService();