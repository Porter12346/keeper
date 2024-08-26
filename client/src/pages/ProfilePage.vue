<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { profilesService } from '@/services/ProfilesService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const profile = computed(() => AppState.activeProfile)
const vaults = computed(() => AppState.vaults)
const keeps = computed(() => AppState.keeps)

onMounted(() => {
    if (!profile.value) profilesService.getProfileById(route.params.profileId);
    getProfileKeeps();
    getProfileVaults();
})

async function getProfileKeeps() {
    try {
        await profilesService.getProfileKeeps(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
    }

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
                <h1 class="text-center">{{ profile.name }}</h1>
                <p class="text-center">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
                <h2>Vaults</h2>
                <div class="row">
                    <div class="col-3 pb-5 px-3" v-for="vault in vaults" :key="vault.id">
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
</template>


<style lang="scss" scoped>
.profile-pic {
    border-radius: 50%;
    margin-top: -7vh;
    box-shadow: 1px 1px 10px black;
}

.grid {
    columns: 12rem 4;
    column-gap: 1rem;
}
</style>