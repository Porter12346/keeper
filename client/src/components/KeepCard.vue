<script setup>
import { Keep } from '@/models/Keep.js';

const props = defineProps({
    keepProp: { type: Keep, required: true }
})
</script>


<template>
    <img class="img-fluid keep-img" :src="keepProp.img" :alt="keepProp.name">
    <div class="d-flex justify-content-between align-items-center neg-marg">
        <p class="mx-2 mb-0 fs-md-4 fw-bold text-light text-shadow my-3">{{ keepProp.name }}</p>
        <img class="profile-pic mx-2 d-md-block d-none" :src="keepProp.creator.picture"
            :alt="keepProp.creator.name">
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

.neg-marg{
    margin-top: -6vh;
}
.text-shadow{
    text-shadow: 1px 1px 10px black;
}
</style>