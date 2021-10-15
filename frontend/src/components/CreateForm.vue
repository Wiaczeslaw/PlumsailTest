<template>
  <div>
    <h1>{{id != '' ? 'Edit' : 'Create'}} form</h1>
    <div class="form">
      <div class="item">
        <label>form name</label>
        <div>
          <input type="text" v-model="form.name">
        </div>
      </div>
      <div v-for="item in templateItems" :key="item.index">
        <div class="item">
          <label>{{item.name}}</label>
          <item
              :value="getValue(item.id)"
              :type="item.type"
              :select-values="item.values"
              :select-value-id="getSelectValue(item.id)"
              :id="getId(item.id)"
              :template-id="item.id"
              @FromSelect= 'catchSelectValue'
              @FromInput= 'catchValue'
          ></item>
        </div>
      </div>
    </div>

    <button type="button" class="btn btn-success" @click="save">
      Save
    </button>

  </div>
</template>

<script>
import Item from './FormItem';
import axios from "axios";
export default {
  name: "Form",
  components: {
    Item
  },
  props: {
    id: {
      type: String,
      default: '',
    },
    templateId: {
      type: String,
      default: '',
    }
  },
  data() {
    return {
      items: [],
      templateItems: [],
      form: {
        id: '',
        name: '',
        items: [],
        templateId: ''
      }
    };
  },
  async mounted() {
      if(this.id != ''){
        const dataForm  = await axios.get(`https://localhost:5001/api/form/get?id=${this.id}`);
        console.log(dataForm)
        this.items = dataForm.data.items;
        this.form.name = dataForm.data.name;
      }

      const dataTemplate = await axios.get(`https://localhost:5001/api/template/get?id=${this.templateId}`);
      this.templateItems = dataTemplate.data.items;
      console.log(this.templateItems)
      this.form.id = this.id ?? '';
      this.form.templateId = this.templateId;

  },
  methods: {
    getValue(id){
      let item = this.items.find(x => x.templateId == id);
      if(item != undefined){
        return item.value;
      }else{
        return ''
      }
    },
    getSelectValue(id){
      let item = this.items.find(x => x.templateId == id);
      if(item != undefined){
        return item.selectValueId;
      }else{
        return ''
      }
    },
    getId(id){
      let item = this.items.find(x => x.templateId == id);
      if(item != undefined){
        return item.id;
      }else{
        return ''
      }
    },
    catchSelectValue(value, id, templateId){
      console.log(templateId)
      const item = this.form.items.find(x => x.templateId == templateId);
      if(item == undefined){
        this.form.items.push({id: id, formItemTemplateId: templateId, FormItemSelectValueId: value});
      }else{
        item.FormItemSelectValueId = value
      }
    },
    catchValue(value, id, templateId){
      console.log(templateId)
      const item = this.form.items.find(x => x.templateId == templateId);
      if(item == undefined){
        this.form.items.push({id: id, formItemTemplateId: templateId, value: value});
      }else{
        item.value = value
      }
    },
    save() {

      let form = this.form;
      console.log(form)
      if (this.id != ''){
        axios.post('https://localhost:5001/api/form/update', form)
            .then(() => {
              //need to handle response code
              alert('success!');
            })
      }else{
        axios.post('https://localhost:5001/api/form/create', form)
            .then(() => {
              //need to handle response code
              alert('success!');
            })
      }
    }
  }
}
</script>

<style scoped>
.form {
  border: black 1px solid;
  align-content: center;
  margin-bottom: 20px;
  padding: 30px;
}

.item {
  margin-bottom: 20px;
}

.btn {
  margin-top: 20px;
}
</style>