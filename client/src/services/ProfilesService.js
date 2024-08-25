import { Account } from "@/models/Account.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { Keep } from "@/models/Keep.js"

class ProfilesService{
    async getProfileVaults(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        const vaults = response.data.map((data)=>new Vault(data))
        AppState.vaults = vaults
    }
    
    async getProfileKeeps(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        const keeps = response.data.map((data)=>new Keep(data))
        AppState.keeps = keeps
    }

    setProfile(creator) {
        AppState.activeProfile = creator
    }

    async getProfileById(id) {
        const response = await api.get(`api/profiles/${id}`)
        const profile = new Account(response.data)
        AppState.activeProfile = profile
    }
}
export const profilesService = new ProfilesService()