function SetTableDescriptions(name, paging, ordering, info, searching) {
	$(name).DataTable({
		"paging": paging,
		"ordering": ordering,
		"info": info,
		"searching": searching,
		"language": {
			"lengthMenu": "Mostrando _MENU_ registros por página",
			"zeroRecords": "Nenhum registro encontrado",
			"info": "Mostrando página _PAGE_ de _PAGES_",
			"infoEmpty": "Nenhum registro disponível",
			"infoFiltered": "(filtrando de _MAX_ registros no total)"
		}
	});
}