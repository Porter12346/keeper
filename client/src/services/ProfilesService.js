import { Account } from "@/models/Account.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class ProfilesService{
    setProfile(creator) {
        AppState.activeProfile = creator
    }
    async getProfileById(id) {
        const response = await api.get(`api/profile/${id}`)
        const profile = new Account(response.data)
        AppState.activeProfile = profile
    }
}
export const profilesService = new ProfilesService()