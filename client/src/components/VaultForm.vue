<script setup>
import { imgUploadService } from '@/services/ImgUploadService.js';
import { vaultsService } from '@/services/VaultsService.js';
import Pop from '@/utils/Pop.js';
import { ref } from 'vue';

const editableVaultData = ref({
    name: '',
    img: '',
    description: '',
    isPrivate: false
})

const img = ref(null)

async function selectFile(event) {
    try {
        console.log("selecting img")
        img.value = event.target.files[0]
    }
    catch (error) {
        Pop.error(error);
    }

}

async function createVault() {
    try {
        editableVaultData.value.img = await uploadImg()
        await vaultsService.createVault(editableVaultData.value)
        Pop.success("Vault Created")
        editableVaultData.value = ({
            name: '',
            img: '',
            description: '',
            isPrivate: false
        })
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
    <!-- Modal -->
    <div class="modal fade" id="vaultForm" tabindex="-1" aria-labelledby="vaultFormLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="vaultFormLabel">Add your vault</h1>
                    <button type="button" class="btn-close" title="close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="mb-3">
                            <input v-model="editableVaultData.name" placeholder="Title..." type="string"
                                class="form-control border-0 border-bottom rounded-0" id="vaultFormTitle"
                                aria-describedby="TitleHelp" required minlength="1" maxlength="255">
                        </div>
                        <div class="mb-1 pb-2">
                            <input @change="selectFile" required="true" name="files" class="form-control" type="file" multiple="false"
                                accept="image/*">
                        </div>
                        <div class="mb-3 pt-5">
                            <textarea v-model="editableVaultData.description" placeholder="Vault Description..."
                                type="string" class="form-control border-0 border-bottom rounded-0"
                                id="vaultFormDescription" rows="1" required minlength="1" maxlength="1000"></textarea>
                        </div>
                        <div class="text-end">
                            <small class="text-body-tertiary">Private Vaults can only be seen by you</small>
                            <div class="mb-3 form-check d-flex align-items-center gap-2 pe-4 justify-content-end">
                                <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input"
                                    id="exampleCheck1">
                                <label class="form-check-label fs-5 pb-0" for="exampleCheck1">Make Vault
                                    Private?</label>
                            </div>
                        </div>
                        <div class="text-end pe-4">
                            <button type="submit" class="btn btn-dark ">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>