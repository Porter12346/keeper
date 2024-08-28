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
    <div type="button" @click="goToVault()">
        <div class="position-relative">
            <img class="img-fluid img-style rounded shadow" :src="vaultProp.img" :alt="vaultProp.name">
            <p class="position-absolute text-pos fw-bold text-light ps-2 fs-md-4 fs-6">{{ vaultProp.name }}</p>
        </div>

    </div>
</template>


<style lang="scss" scoped>
.text-pos {
    bottom: 0%;
    width: 100%;
    max-height: 8vh;
    overflow: hidden;
    text-shadow: 0px 0px 6px black;
}

.img-style {
    aspect-ratio: 5/3;
    object-fit: cover;
    object-position: center;
}
</style>