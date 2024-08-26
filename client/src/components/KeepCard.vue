<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import ProfilePage from '@/pages/ProfilePage.vue';
import { keepsService } from '@/services/KeepsService.js';
import { profilesService } from '@/services/ProfilesService.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()
const account = computed(() => AppState.account)

const props = defineProps({
    keepProp: { type: Keep, required: true },
    showCreator: { type: Boolean, default: true }
})

async function openKeep() {
    try {
        await keepsService.setActive(props.keepProp)
        Modal.getOrCreateInstance("#keepModal").show()
    }
    catch (error) {
        Pop.error(error);
    }
}

async function openProfile() {
    try {
        await profilesService.setProfile(props.keepProp.creator)
        router.push({ name: 'Profile', params: { profileId: props.keepProp.creator.id } })
    }
    catch (error) {
        Pop.error(error);
    }

}

async function deleteKeep() {
    try {
        await keepsService.deleteKeep(props.keepProp.id)
    }
    catch (error) {
        Pop.error(error);
    }
}
// data-bs-toggle="modal" data-bs-target="#"

</script>


<template>
    <div class="position-relative">
        <img class="img-fluid keep-img rounded" :src="keepProp.img" :alt="keepProp.name" @click="openKeep()"
            type="button">
        <div @click="openKeep()" type="button" class="d-flex justify-content-between align-items-center position-absolute positioning">
            <p  type="button" class="mx-2 mb-4 fs-md-4 fw-bold text-light text-shadow my-3 fs-5 maxWidth">{{ keepProp.name }}</p>
            <img v-if="showCreator && keepProp.creator" @click.stop="openProfile()" type="button"
                class="profile-pic mx-2 d-md-block d-none shadow" :src="keepProp.creator.picture"
                :alt="keepProp.creator.name">
        </div>
        <div v-if="keepProp.creator && showCreator" >
            <i v-if="account?.id == keepProp.creator.id" @click="deleteKeep()" type="button" style="top: 0%; right: 3%; text-shadow: 0px 0px 8px black;"
                class="position-absolute mdi mdi-close-circle text-danger"></i>
        </div>
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

.positioning {
    bottom: 0%;
    width: 100%;
    max-height: 8vh;
    overflow: hidden;
}

.text-shadow {
    text-shadow: 1px 1px 10px black;
}

.maxWidth {
    max-width: 60%;
}
</style>