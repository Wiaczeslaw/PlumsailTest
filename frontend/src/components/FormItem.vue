<template>
  <div v-if="inputType != 'select'" @change="toForm($event, 'FromInput')">
    <input :type="inputType" :value="value">
  </div>
  <div v-else>
    <select class="form-select" v-model="selectValue" @change="toForm($event, 'FromSelect')">
      <option v-for="sv in selectValues" :key="sv.id" v-bind:value="sv.id" :selected="sv.id == selectValueId">{{ sv.value}}</option>
    </select>
  </div>

</template>

<script>


export default {
  name: "FormItem",
  props: {
    id: {
      type: String,
      default: '',
    },
    value: {
      default: '',
    },
    type: {
      type: Number,
      default: 0,
    },
    selectValueId: {
      type: String,
      default: '',
    },
    selectValues: {
      default: [],
    },
    templateId: {
      type: String,
      default: '',
    }
  },
  methods:{
    toForm(event, cName){
      this.$emit(cName, event.target.value, this.id, this.templateId)
    }
  },
  data() {
    return {
      inputType: '',
      selectValue: this.selectValueId
    };
  },
  async mounted() {

    switch (this.type) {
      case 0:
        this.inputType = 'text'
        break;
      case 1:
        this.inputType = 'select'
        break;
      case 2:
        this.inputType = 'checkbox'
        break;
      case 4:
        this.inputType = 'text'
        break;
      case 5:
        this.inputType = 'textArea'
        break;
      case 6:
        this.inputType = 'radio'
        break;
      case 7:
        this.inputType = 'date'
        break;
      default:
        this.inputType = 'text';
    }
  }
}
</script>

<style scoped>

</style>
