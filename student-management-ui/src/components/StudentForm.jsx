import React, { useState, useEffect } from 'react';
import { studentService } from '../services/api';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Grid,
  Box,
} from '@mui/material';

const StudentForm = ({ open, student, onClose, onSuccess, onError }) => {
  const [formData, setFormData] = useState({
    nome: '',
    idade: '',
    serie: '',
    notaMedia: '',
    endereco: '',
    nomePai: '',
    nomeMae: '',
    dataNascimento: '',
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (student) {
      setFormData({
        nome: student.nome || '',
        idade: student.idade || '',
        serie: student.serie || '',
        notaMedia: student.notaMedia || '',
        endereco: student.endereco || '',
        nomePai: student.nomePai || '',
        nomeMae: student.nomeMae || '',
        dataNascimento: student.dataNascimento
          ? new Date(student.dataNascimento).toISOString().split('T')[0]
          : '',
      });
    } else {
      setFormData({
        nome: '',
        idade: '',
        serie: '',
        notaMedia: '',
        endereco: '',
        nomePai: '',
        nomeMae: '',
        dataNascimento: '',
      });
    }
  }, [student, open]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      const studentData = {
        ...formData,
        idade: parseInt(formData.idade),
        serie: parseInt(formData.serie),
        notaMedia: parseFloat(formData.notaMedia),
        dataNascimento: new Date(formData.dataNascimento).toISOString(),
      };

      if (student) {
        studentData.id = student.id;
        await studentService.updateStudent(student.id, studentData);
        onSuccess('Estudante atualizado com sucesso');
      } else {
        await studentService.createStudent(studentData);
        onSuccess('Estudante criado com sucesso');
      }
    } catch (err) {
      onError(err.response?.data?.message || 'Erro ao salvar estudante');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={() => onClose()} maxWidth="md" fullWidth>
      <DialogTitle>
        {student ? 'Editar Estudante' : 'Adicionar Novo Estudante'}
      </DialogTitle>
      <Box component="form" onSubmit={handleSubmit}>
        <DialogContent>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                label="Nome"
                name="nome"
                value={formData.nome}
                onChange={handleChange}
              />
            </Grid>
            <Grid item xs={12} sm={3}>
              <TextField
                required
                fullWidth
                type="number"
                label="Idade"
                name="idade"
                value={formData.idade}
                onChange={handleChange}
                inputProps={{ min: 1, max: 100 }}
              />
            </Grid>
            <Grid item xs={12} sm={3}>
              <TextField
                required
                fullWidth
                type="number"
                label="Série"
                name="serie"
                value={formData.serie}
                onChange={handleChange}
                inputProps={{ min: 1, max: 12 }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                type="number"
                label="Nota Média"
                name="notaMedia"
                value={formData.notaMedia}
                onChange={handleChange}
                inputProps={{ min: 0, max: 10, step: 0.1 }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                type="date"
                label="Data de Nascimento"
                name="dataNascimento"
                value={formData.dataNascimento}
                onChange={handleChange}
                InputLabelProps={{ shrink: true }}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                label="Endereço"
                name="endereco"
                value={formData.endereco}
                onChange={handleChange}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                label="Nome do Pai"
                name="nomePai"
                value={formData.nomePai}
                onChange={handleChange}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                label="Nome da Mãe"
                name="nomeMae"
                value={formData.nomeMae}
                onChange={handleChange}
              />
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => onClose()} disabled={loading}>
            Cancelar
          </Button>
          <Button type="submit" variant="contained" disabled={loading}>
            {loading ? 'Salvando...' : 'Salvar'}
          </Button>
        </DialogActions>
      </Box>
    </Dialog>
  );
};

export default StudentForm;
