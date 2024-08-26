import Pop from "@/utils/Pop.js"
import { api } from "./AxiosService.js"
import { Modal } from "bootstrap"
import { AppState } from "@/AppState.js"

class VaultKeepService{
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