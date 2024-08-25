<script setup>
import { AppState } from '@/AppState.js';
import { profilesService } from '@/services/ProfilesService.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const profile = computed(()=>AppState.activeProfile)
</script>



<template>
<div class="container">
    <div class="row">
        <div class="col-12">
            <img :src="profile.coverImg" :alt="profile.name">
        </div>
    </div>
</div>
</template>


<style lang="scss" scoped>

</style>