<template>
  <div class="list row">
    <div class="col-md-8">
      <div class="input-group mb-3">
        <input
            type="text"
            class="form-control"
            placeholder="Search by title"
            v-model="search"
        />
        <div class="input-group-append">
          <button
              class="btn btn-outline-secondary"
              type="button"
              @click="searchTitle"
          >
            Search
          </button>
        </div>
      </div>
    </div>
    <div class="col-md-8">
      <div>
        <h1>Your forms</h1>

        <table class="table table-striped">
          <thead>
          <tr>
            <th>#</th>
            <th>type</th>
            <th>name</th>
          </tr>
          </thead>
          <tbody>
          <router-link :to="`form/${form.templateId}/${form.id}`" tag="tr" v-for="(form,i) in forms" :key="i">

            <th scope="row">{{ i }}</th>
            <td>{{ form.templateName }}</td>
            <td>{{ form.name }}</td>
          </router-link>
          </tbody>
        </table>
      </div>
      <div class="input-group">
        <div class="input-group-prepend">
          <label class="input-group-text" for="inputGroupSelect01">template</label>
        </div>
        <select class="custom-select" v-model="selectTemplate" id="inputGroupSelect01">
          <option v-for="template in templates" :key="template.id" v-bind:value="template.id" :selected="template.id == selectTemplate">{{template.name}}</option>
        </select>
        <div class="input-group-append" for="inputGroupSelect01">
          <router-link
              class="btn btn-outline-secondary"
              :to="`form/${selectTemplate}`"
              tag="button"
              type="button"
          >Create form</router-link>
        </div>
      </div>


    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: 'Forms',
  methods:{
    searchTitle: async function (){
      const {data} = await axios.get(`https://localhost:5001/api/form/get/list?search=${this.search}`);
      this.forms = data;
    }
  },
  data() {
    return {
      forms: null,
      search: '',
      selectTemplate: '',
      templates: null
    };
  },
  async mounted() {

    const [dataForm, dataTemplate] = await Promise.all([
      axios.get(`https://localhost:5001/api/form/get/list`),
      axios.get(`https://localhost:5001/api/template/get/list`)
    ]);

    this.forms = dataForm.data;
    this.templates = dataTemplate.data;
  },
}

</script>

<style scoped>
.list {
  text-align: left;
  max-width: 750px;
  margin: auto;
}
</style>
