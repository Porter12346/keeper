import { Keep } from "@/models/Keep.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import Pop from "@/utils/Pop.js";

class KeepsService{
    async deleteKeep(id) {
        const conf = await Pop.confirm("Are you sure you would like to delete this keep")
        if(!conf)throw new Error("canceled")
        await api.delete(`api/keeps/${id}`)
        const index = AppState.keeps.findIndex((keep)=>keep.id == id)
        AppState.keeps.splice(index, 1)
    }
    async createKeep(data) {
        const response = await api.post('api/keeps', data)
        const keep = new Keep(response.data)
        AppState.keeps.unshift(keep)
    }
    async setActive(keepProp) {
        keepProp.views++
        AppState.activeKeep = keepProp;
        await api.get(`api/keeps/${keepProp.id}`)
    }

    async getKeeps() {
        AppState.keeps = []
        const response = await api.get('api/keeps')
        const keeps = response.data.map((data)=>new Keep(data))
        AppState.keeps = keeps.reverse()
    }

}
export const keepsService = new KeepsService();