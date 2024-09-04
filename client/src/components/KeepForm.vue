<script setup>
import { imgUploadService } from '@/services/ImgUploadService.js';
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { ref } from 'vue';


const editableKeepData = ref({
    name: "",
    img: "",
    description: ""
})

const img = ref(null)


async function createKeep() {
    try {
        editableKeepData.value.img = await uploadImg()
        await keepsService.createKeep(editableKeepData.value)
        Pop.success("keep created")
        editableKeepData.value = {
            name: "",
            img: "",
            description: ""
        }
    }
    catch (error) {
        Pop.error(error);
    }
}

async function selectFile(event) {
    try {
        console.log("selecting img")
        img.value = event.target.files[0]
    }
    catch (error) {
        Pop.error(error);
    }

}


async function uploadImg() {
    try {
        console.log("uploading img")
        const url = await imgUploadService.uploadImg(img.value)
        return (url)
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="keepFormLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 fw-bold text-body-secondary" id="keepFormLabel">Add your keep</h1>
                    <button type="button" class="btn-close" title="close" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()" class="d-flex flex-column">
                        <div class="mb-3">
                            <input v-model="editableKeepData.name" placeholder="Title..." type="string"
                                class="form-control border-0 border-bottom rounded-0" id="KeepFormTitle"
                                aria-describedby="TitleHelp" required minlength="1" maxlength="255">
                        </div>
                        <div class="mb-3 pt-5">
                            <textarea v-model="editableKeepData.description" placeholder="Keep Description..."
                                type="string" class="form-control border-0 border-bottom rounded-0"
                                id="KeepFormImageURL" rows="1" required minlength="1" maxlength="1000"></textarea>
                        </div>
                        <div class="mb-1 pb-2">
                            <input @change="selectFile" required="true" name="files" class="form-control" type="file" multiple="false"
                                accept="image/*">
                        </div>
                        <div class="text-end">
                            <button type="submit" class="btn btn-dark ">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</template>


<style lang="scss" scoped></style>