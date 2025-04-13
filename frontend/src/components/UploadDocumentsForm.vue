<div
    class="upload-container"
    @dragover.prevent="handleDragOver"
    @dragleave.prevent="handleDragLeave"
    @drop.prevent="handleDrop"
>
<div
    class="upload-box"
    :class="{ 'is-dragging': isDragging }"
    @click="triggerFileInput"
>
  <input
      type="file"
      multiple
      ref="fileInput"
      class="file-input"
      @change="handleFileChange"
  />
  <i class="el-icon-upload" />
  <div class="upload-text">
    Drag your files here or <em>click to choose</em>
  </div>
</div>
<div v-if="fileList.length > 0">
  <h3>Uploaded Files:</h3>
  <ul>
    <li v-for="(file, index) in fileList" :key="file.name + index">
      {{ file.name }} - {{ file.size }} bytes
      <el-button size="small" type="danger" @click="removeFile(index)">Remove</el-button>
    </li>
  </ul>
</div>
</div>

<script setup>
  import {ref} from "vue";

  const props = defineProps({
    form: Object
  })
  
  const fileList = ref([])
  const isDragging = ref(false)
  const fileInput = ref(null)
  
  const removeFile = (index) => {
    fileList.value.splice(index, 1)
  }
  
  function handleFileChange(event) {
    const newFiles = Array.from(event.target.files)
    fileList.value = [...fileList.value, ...newFiles]
    event.target.value = ''
  }
  
  function handleDragOver(event) {
    isDragging.value = true;
  }
  
  function handleDragLeave() {
    isDragging.value = false;
  }
  
  function handleDrop(event) {
    const files = event.dataTransfer.files;
    fileList.value = Array.from(files);
    isDragging.value = false;
  }
  
  function triggerFileInput() {
    fileInput.value?.click()
  }
</script>