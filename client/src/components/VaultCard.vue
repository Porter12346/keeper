<script setup>
import { Vault } from '@/models/Vault.js';
import VaultsPage from '@/pages/VaultPage.vue';
import { vaultsService } from '@/services/VaultsService.js';
import { useRouter } from 'vue-router';

const router = useRouter()
const props = defineProps({ vaultProp: { type: Vault, required: true } })

async function goToVault() {
    vaultsService.setActiveVault(props.vaultProp)
    router.push({ name: 'Vault', params: { vaultId: props.vaultProp.id } })
}
</script>


<template>
    <div @click="goToVault()">
        <img type="button" class="img-fluid img-style rounded shadow" :src="vaultProp.img" :alt="vaultProp.name">
        <h4 class="neg-mar text-light ps-2">{{ vaultProp.name }}</h4>
    </div>
</template>


<style lang="scss" scoped>
.neg-mar {
    margin-top: -5vh;
    text-shadow: 1px 1px 5px black;
}

.img-style {
    aspect-ratio: 4/2;
    object-fit: cover;
    object-position: center;
}
</style>