import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async editAccountData(editAccountData) {
      const response = await api.put(`account`, editAccountData)
      const account = new Account(response.data)
      AppState.account = account
      AppState.activeProfile = account
  }
  async setAccountVaults() {
    await this.getAccountVaults()
    logger.log(AppState.accountVaults)
      AppState.vaults = AppState.accountVaults
  }
  async getAccountVaults(){
      const response = await api.get(`account/vaults`)
      AppState.accountVaults = response.data.map((data)=>new Vault(data))
  }
  async getAccount() {
    try {
      this.getAccountVaults()
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
