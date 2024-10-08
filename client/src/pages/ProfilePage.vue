<script setup>
import { AppState } from '@/AppState.js';
import AccountInfoForm from '@/components/AccountInfoForm.vue';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const profile = computed(() => AppState.activeProfile)
const vaults = computed(() => AppState.vaults)
const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)
const identity = computed(() => AppState.identity)

onMounted(() => { getProfile() })

async function getProfileKeeps() {
    try {
        await profilesService.getProfileKeeps(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }

}

async function getProfile() {
    try {
        await profilesService.getProfileById(route.params.profileId);
        getProfileKeeps()
        // @ts-ignore
        if (route.params.profileId != identity.value?.id) {
            logger.log("profile")
            await getProfileVaults();
        }
        else {
            logger.log("account")
            await getAccountVaults();
        }
    }
    catch (error) {
        Pop.error(error);
    }
}

function getAccountVaults() {
    accountService.setAccountVaults()

}

async function getProfileVaults() {
    try {
        await profilesService.getProfileVaults(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>



<template>
    <div v-if="profile" class="container">
        <div class="row">
            <div class="col-12">
                <img class="img-fluid" style="height: 30vh; width: 100%; object-fit: cover; object-position: center;"
                    :src="profile.coverImg" :alt="profile.name">
                <div class="text-center">
                    <img class="profile-pic" :src="profile.picture" :alt="profile.name">
                </div>
                <div class="d-flex text-center justify-content-center gap-3">
                    <h1 class="text-center">{{ profile.name }}</h1>
                    <i v-if="account?.id == profile.id" data-bs-toggle="modal" data-bs-target="#AccountInfo" title="edit account info" role="button" class="mdi mdi-pencil fs-3 text-primary"></i>
                </div>
                <p class="text-center">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
                <h2>Vaults</h2>
                <div class="row">
                    <div class="col-md-3 col-6 pb-5 px-3" v-for="vault in vaults" :key="vault.id">
                        <VaultCard :vault-prop="vault" />
                    </div>
                </div>
                <h2>Keeps</h2>
                <div class="grid pb-3">
                    <div class="item my-4 mx-1" v-for="keep in keeps" :key="keep.id">
                        <KeepCard :keep-prop="keep" :show-creator="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-if="account">
    <AccountInfoForm :account="account" />
    </div>
</template>


<style lang="scss" scoped>
.profile-pic {
    border-radius: 50%;
    margin-top: -7vh;
    box-shadow: 1px 1px 10px black;
    width: 25vw;
    max-width: 200px;
    aspect-ratio: 1/1;
    object-fit: cover;
}

.grid {
    columns: 12rem 4;
    column-gap: 1rem;
}
</style>