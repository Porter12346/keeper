<script setup>
import { Keep } from '@/models/Keep.js';
import ProfilePage from '@/pages/ProfilePage.vue';
import { keepsService } from '@/services/KeepsService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { Modal } from 'bootstrap';
import { useRouter } from 'vue-router';

const router = useRouter()

const props = defineProps({
    keepProp: { type: Keep, required: true }
})

async function openKeep() {
    await keepsService.setActive(props.keepProp)
    Modal.getOrCreateInstance("#keepModal").show()
}

async function openProfile() {
    await profilesService.setProfile(props.keepProp.creator)
    router.push({name: 'Profile', params: {profileId: props.keepProp.id}})
}
// data-bs-toggle="modal" data-bs-target="#"

</script>


<template>
    <img class="img-fluid keep-img rounded" :src="keepProp.img" :alt="keepProp.name" @click="openKeep()" type="button">
    <div class="d-flex justify-content-between align-items-center neg-marg">
        <p class="mx-2 mb-3 fs-md-4 fw-bold text-light text-shadow my-3">{{ keepProp.name }}</p>
        <img @click="openProfile()" type="button" class="profile-pic mx-2 d-md-block d-none" :src="keepProp.creator.picture" :alt="keepProp.creator.name">
    </div>
</template>


<style lang="scss" scoped>
.keep-img {
    width: 100%;
    height: auto;
}

.profile-pic {
    border-radius: 50%;
    height: 5vh;
    aspect-ratio: 1/1;
}

.neg-marg {
    margin-top: -7vh;
}

.text-shadow {
    text-shadow: 1px 1px 10px black;
}
</style>