import { Vault } from "@/models/Vault.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"
import Pop from "@/utils/Pop.js"
import { Account } from "@/models/Account.js"

class VaultsService{
    async deleteVault(id) {
        const confirm = await Pop.confirm("Are you sure you want to delete this vault")
        if(!confirm)throw new Error("Canceled");
        api.delete(`api/vaults/${id}`)
        const index = AppState.vaults.findIndex((vault)=>vault.id == id)
        AppState.vaults.splice(index, 1)
        const index2 = AppState.accountVaults.findIndex((vault)=>vault.id == id)
        AppState.accountVaults.splice(index, 1)
        Pop.success("Deleted")
    }
    async getVaultById(id) {
        AppState.keeps = []
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${id}`)
        const vault = new Vault(response.data)
        AppState.activeVault = vault
    }
    async getVaultKeeps(id) {
        const response = await api.get(`api/vaults/${id}/keeps`)
        const keeps = response.data.map((data)=>new Keep(data))
        AppState.keeps = keeps
    }
    setActiveVault(vaultProp) {
        vaultProp.creator = AppState.activeProfile
        AppState.activeVault = vaultProp
    }

    async createVault(data) {
        const response = await api.post('api/vaults', data)
        const vault = new Vault(response.data)
        AppState.vaults.unshift(vault)
    }
    
}
export const vaultsService = new VaultsService()