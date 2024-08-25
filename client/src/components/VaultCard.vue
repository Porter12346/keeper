<script setup>
import { Vault } from '@/models/Vault.js';
import VaultsPage from '@/pages/VaultPage.vue';

const props = defineProps({ vaultProp: { type: Vault, required: true } })
</script>


<template>
    <router-link :to="{name: 'Vault',  params: {vaultId: vaultProp.id}}">
        <img class="img-fluid img-style rounded shadow" :src="vaultProp.img" :alt="vaultProp.name">
        <h3 class="neg-mar text-light ps-2">{{ vaultProp.name }}</h3>
    </router-link>
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