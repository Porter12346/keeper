import { Vault } from "@/models/Vault.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class VaultsService{
    async createVault(data) {
        const response = await api.post('api/vaults', data)
        const vault = new Vault(response.data)
        AppState.vaults.unshift(vault)
    }
    
}
export const vaultsService = new VaultsService()