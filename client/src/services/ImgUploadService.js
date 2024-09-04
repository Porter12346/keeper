import { api } from "./AxiosService.js"

class ImgUploadService{
async uploadImg(img)
{
    const payload = new FormData()
    payload.append("file", img)
    const response = await api.post("api/upload", payload)
    return(response.data)
}
}
export const imgUploadService = new ImgUploadService()