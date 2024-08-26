import { Keep } from "@/models/Keep.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class KeepsService{
    async createKeep(data) {
        const response = await api.post('api/keeps', data)
        const keep = new Keep(response.data)
        AppState.keeps.unshift(keep)
    }
    setActive(keepProp) {
        AppState.activeKeep = keepProp;
    }

    async getKeeps() {
        AppState.keeps = []
        const response = await api.get('api/keeps')
        const keeps = response.data.map((data)=>new Keep(data))
        AppState.keeps = keeps.reverse()
    }

}
export const keepsService = new KeepsService();