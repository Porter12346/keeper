<script setup>
import { AppState } from '@/AppState.js';
import { vaultKeepService } from '@/services/VaultKeepService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { Value } from 'sass';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const vaults = computed(() => AppState.accountVaults)
const vault = computed(()=>AppState.activeVault)

function toProfile() {
    Modal.getOrCreateInstance('#keepModal').hide()
    router.push({ name: "Profile", params: { profileId: keep.value.creator.id } })
}

const selectedVault = ref()

async function createVaultKeep() {
    try {
        vaultKeepService.createVaultKeep(keep.value.id, selectedVault.value)
        keep.value.kept++
    }
    catch (error) {
        Pop.error(error);
    }

}

async function deleteVaultKeep() {
    try {
        await vaultKeepService.deleteVaultKeep(keep.value.vaultKeepId)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div class="modal modal-xl fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div v-if="keep" class="modal-dialog">
            <div class="modal-content">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6 col-12 p-0">
                            <img class="img-fluid w-100 img-sizing rounded " :src="keep.img" :alt="keep.name">
                        </div>
                        <div class="col-lg-6 col-12 p-0 d-flex flex-column justify-content-between px-4">
                            <div>
                                <p class="fs-4 text-center d-flex justify-content-center gap-4 pb-3 pb-md-0 pt-md-5"><span
                                        class="mdi mdi-eye">{{ keep.views }}</span><span
                                        class="mdi mdi-safe-square-outline">{{ keep.kept }}</span></p>
                            </div>
                            <div class="pb-5 pb-md-0">
                                <h1 class="text-center">
                                    {{ keep.name }}
                                </h1>
                                <p class="text-center">
                                    {{ keep.description }}
                                </p>
                            </div>
                            <div class="d-flex justify-content-around align-items-center pb-3">
                                <div v-if="keep.vaultKeepId && vaults.find((vaultt)=>vaultt.id == vault.id ) ">
                                    <p @click="deleteVaultKeep()" class="border-bottom mb-0" type="button"><i
                                            class="mdi mdi-cancel"></i> Remove
                                    </p>
                                </div>
                                <div v-else-if="account" class="d-flex gap-3 align-items-center">
                                    <form class="d-flex gap-3 align-items-center" @submit.prevent="createVaultKeep()">
                                        <select v-model="selectedVault" name="selectVault"
                                            class="form-select border-0 border-bottom rounded-0"
                                            aria-label="Select Vault Name" style="outline: none;">
                                            <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{
                                                vault.name }}</option>
                                        </select>
                                        <button class="btn btn-secondary">save</button>
                                    </form>
                                </div>
                                <div v-if="keep.creator" class="d-flex align-items-center">
                                    <img @click="toProfile()" type="button" class="profile-pic ms-5 mx-2 pic-shadow"
                                        :src="keep.creator.picture" :alt="keep.creator.name">
                                    <p class="mb-0 fw-bold">{{ keep.creator.name }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.profile-pic {
    height: 5vh;
    border-radius: 50%;
}

.img-sizing {
    height: 100%;
    object-fit: cover;
    object-position: center;
}

.pic-shadow {
    box-shadow: 0px 0px 12px black;
}
</style>