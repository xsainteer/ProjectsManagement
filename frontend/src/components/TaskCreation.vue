<template>
  <el-card class="max-w-2xl mx-auto p-6 shadow-lg rounded-2xl">
    <h2 class="text-2xl font-bold mb-6">Task creation</h2>

    <el-form label-position="top" class="space-y-4">

      <!-- Проект -->
      <el-form-item label="Project">
        <el-select
            v-model="selectedProject"
            filterable
            remote
            placeholder="Select a project"
            :remote-method="(query) => fetchProjects(query)"
            :loading="loading"
            class="w-full"
        >
          <el-option
              v-for="item in projectOptions"
              :key="item.id"
              :label="item.name"
              :value="item"
          />
        </el-select>
        <div v-if="selectedProject">
          Chosen project - {{selectedProject.name}}
        </div>
      </el-form-item>

      <!-- Автор -->
      <el-form-item label="Assigner">
        <el-select
            v-model="selectedAssigner"
            filterable
            remote
            placeholder="Choose an assigner"
            :remote-method="fetchEmployees"
            :loading="loading"
            class="w-full"
        >
          <el-option
              v-for="item in employeeOptions"
              :key="item.id"
              :label="item.name"
              :value="item"
          />
        </el-select>
        <div v-if="selectedAssigner">
          Chosen assigner - {{selectedAssigner.name}}
        </div>
      </el-form-item>

      <!-- Исполнитель -->
      <el-form-item label="Assignee">
        <el-select
            v-model="selectedAssignee"
            filterable
            remote
            placeholder="Select an"
            :remote-method="fetchEmployees"
            :loading="loading"
            class="w-full"
        >
          <el-option
              v-for="item in employeeOptions"
              :key="item.id"
              :label="item.name"
              :value="item"
          />
        </el-select>
        <div v-if="selectedAssignee">
          Chosen assigner - {{selectedAssignee.name}}
        </div>
      </el-form-item>

      <el-form-item label="Task Name">
        <el-input v-model="taskName" placeholder="Enter task name" />
      </el-form-item>

      <el-form-item label="Task Description">
        <el-input v-model="taskDescription" placeholder="enter task description" />
      </el-form-item>

      <el-form-item label="Comment">
        <el-input v-model="taskComment" placeholder="enter task comment" />
      </el-form-item>
      
      <el-form-item label="Status">
        <el-select v-model="taskStatus" placeholder="Select Status" class="w-full">
          <el-option label="ToDo" value="ToDo" />
          <el-option label="InProgress" value="InProgress" />
          <el-option label="Done" value="Done" />
        </el-select>
      </el-form-item>

      <el-form-item label="Priority">
        <el-select v-model="priority" placeholder="Select priority" class="w-full">
          <el-option label="Low" value="1" />
          <el-option label="Medium" value="2" />
          <el-option label="High" value="3" />
        </el-select>
      </el-form-item>

      <el-form-item>
        <el-button
            type="primary"
            @click="submitTask"
            :loading="loading"
            :disabled="loading"
        >
          {{ loading ? "Creating..." : "Create task" }}
        </el-button>
      </el-form-item>

    </el-form>
  </el-card>
</template>

<script setup>
import axios from "axios";
import {ref} from "vue";

const apiUrl = import.meta.env.VITE_API_URL
  
function resetForm()
{
  taskName.value = ''
  taskStatus.value = ''
  priority.value = ''
  taskDescription.value = ''
  selectedProject.value = null
  selectedAssigner.value = null
  selectedAssignee.value = null
  taskComment.value = null
  
  alert("task created")
}

async function submitTask()
{
  loading.value = true;
  try{
    const task = {
      name: taskName.value,
      description: taskDescription.value,
      authorId: selectedAssigner.value.id,
      executorId: selectedAssignee.value.id,
      projectId: selectedProject.value.id,
      comment: taskComment.value
    }
    
    await axios.post(`${apiUrl}/api/tasks`, task)

    resetForm();
  }
  catch (e) {
    console.error(e);
  }
  finally {
    loading.value = false;
  }
}

const taskName = ref()
const taskStatus = ref()
const priority = ref()
const taskDescription = ref()
const taskComment = ref()

const loading = ref(false)
const selectedProject = ref()
const projectOptions = ref([])
const selectedAssigner = ref()
const selectedAssignee = ref()
const employeeOptions = ref()

async function fetchEmployees(query)
{
    try {
      const response = await axios.get(`${apiUrl}/api/employees`,{
        params: {
          query: query,
        projectId: selectedProject.value.id
        }
      })
      
      employeeOptions.value = response.data;
    }
    catch (error) {
      console.error('Error fetching employees:', error)
      return []
    }
}

async function fetchProjects(query, skip = 0, count = 10)
{
  try {
    const response = await axios.get(`${apiUrl}/api/projects`, {
      params: {
        query,
        skip,
        count
      }
    })

    projectOptions.value = response.data
    
  } catch (error) {
    console.error('Error fetching projects:', error)
    return []
  }
}

</script>