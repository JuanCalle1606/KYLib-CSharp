# Changelog

## v1.6.0

### Arreglado
- Arreglado CreateProcess de Bash ya que con los valores que tenia no se creaban los procesos correctamente.
- Arreglado un fallo en Bash que producia una StackOverflowException en el metodo RunCommand.

### Agregado
- Agregado el atributo AutoLoad  que permite llamar metodos en tipos cuando se carga su ensamblado.
- Agregado el metodo To<T> a IEnumerableExtensions.
- Creada la clase Ensure.

### Eliminado
- Se ha removido la estructura BitArray.