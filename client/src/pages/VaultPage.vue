<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const vault = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.keeps)
const route = useRoute()
const router = useRouter()

onMounted(() => getVault())

async function getVault() {
    try {
        await vaultsService.getVaultById(route.params.vaultId)
        getVaultKeeps()
    }
    catch (error) {
        Pop.error(error);
        router.push({ name: "Home" })
    }

}

async function getVaultKeeps() {
    try {
        await vaultsService.getVaultKeeps(vault.value.id)
    }
    catch (error) {
        Pop.error(error);
    }

}

async function deleteVault() {
    try {
        await vaultsService.deleteVault(vault.value.id)
        router.push({ name: "Home" })
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div v-if="vault">
        <div class="container">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-md-8 col-12 position-relative p-0">
                    <img class="coverImg rounded" :src="vault.img" :alt="vault.name">
                    <div class="position-absolute positioning">
                        <h1 class="text-center text-light text-shadow">{{ vault.name }}</h1>
                        <p class="text-center text-light text-shadow">By {{ vault.creator.name }}</p>
                    </div>
                    <div class="position-absolute positioning2">
                        <i @click="deleteVault()" class="mdi mdi-close-circle fs-1 text-danger " type="button"></i>
                    </div>
                </div>
                <div class="text-center pt-3">
                    <span class="text-center bg-info-subtle rounded-5 p-2">{{ keeps?.length }} Keeps</span>
                    <div class="grid pb-3 pt-5">
                        <div class="item my-4 mx-1" v-for="keep in keeps" :key="keep.id">
                            <KeepCard :keep-prop="keep" :show-creator="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <h1>Loading <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>

</template>


<style lang="scss" scoped>
.text-shadow {
    text-shadow: 1px 1px 5px black;
}

.positioning {
    width: 100%;
    top: 50%;
}

.grid {
    columns: 12rem 4;
    column-gap: 1rem;
}

.coverImg {
    width: 100%;
    object-fit: cover;
    object-position: center;
    aspect-ratio: 6/2;
}

.positioning2 {
    top: 0%;
    left: 93%;
    text-shadow: 0px 0px 8px black;
}
</style>