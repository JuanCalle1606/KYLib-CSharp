# Changelog

## v2.0.0

### Arreglado
- Arreglado CreateProcess de Bash ya que con los valores que tenia no se creaban los procesos correctamente.
- Arreglado un fallo en Bash que producia una StackOverflowException en el metodo RunCommand.
- Arreglado un replace innecesario en Bash que causaba errores en la invocación.

### Agregado
- Agregado el atributo AutoLoad  que permite llamar metodos en tipos cuando se carga su ensamblado.
- Agregado el metodo To<T> a IEnumerableExtensions.
- Creada la clase Ensure.
- Agregados operadores implicitos a la clase Assets.

### Eliminado
- Se ha removido la estructura BitArray.

### Cambiado
- Cambiada la forma de obtener información del sistema en Info.
- Todo lo relacionado con la clase Mod se ha movido a un namespace propio (Modding).
- 