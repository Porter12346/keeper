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
        if(!vault.value)router.push("Home")
        getVaultKeeps()
    }
    catch (error) {
        Pop.error(error);
        router.push({name: "Home"})
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
</script>


<template>
    <div v-if="vault">
        <div class="container">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-md-8 col-12">
                    <img class="coverImg" :src="vault.img" :alt="vault.name">
                    <div class="neg-mar">
                        <h1 class="text-center text-light text-shadow">{{ vault.name }}</h1>
                        <p class="text-center text-light text-shadow">By {{ vault.creator.name }}</p>
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

.neg-mar {
    margin-top: -9vh;
}

.grid {
    columns: 12rem 4;
    column-gap: 1rem;
}
.coverImg{
    width: 100%;
    object-fit: cover;
    object-position: center;
    aspect-ratio: 6/2;
}
</style>