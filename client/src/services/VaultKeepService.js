import Pop from "@/utils/Pop.js"
import { api } from "./AxiosService.js"
import { Modal } from "bootstrap"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class VaultKeepService{
    async createVaultKeep(keepId, vaultId) {
        await api.post('api/vaultkeeps', {keepId, vaultId})
        // const VaultKeep = new Keep(response.data)
        Pop.success("added to vault")
    }
    async deleteVaultKeep(vaultKeepId) {
        const confirm = await  Pop.confirm("Are you sure you would like to remove this keep from this vault")
        if(!confirm)throw new Error("canceled")
        await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        const index = AppState.keeps.findIndex((keep)=>keep.vaultKeepId = vaultKeepId)
        AppState.keeps.splice(index, 1)
        Pop.success("keep removed")
        Modal.getOrCreateInstance("#keepModal").hide()
    }
}
export const vaultKeepService = new VaultKeepService()