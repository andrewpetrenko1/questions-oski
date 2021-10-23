import { Component, OnInit } from '@angular/core';
import { Question } from '../interfaces/question';
import { QuestionService } from '../services/question.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.sass']
})
export class TestListComponent implements OnInit {
  listCount: number = 3;
  listOfQuestions: [Question[]] = [[]];
  confirmCheck = false;

  constructor(
    private questionService: QuestionService,
    private modalService: NgbModal,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.questionService.getList().subscribe(data => this.listOfQuestions = data);
  }

  startTest(index: number, modal: any) {
    let modalRef = this.modalService.open(modal, { centered: true, size: 'sm' });
    modalRef.result.then((result) => {
      if(this.confirmCheck === true) {
        this.questionService.quetionsToAnswer = this.listOfQuestions[index];
        this.router.navigate(['test-answer']);
      }
      this.confirmCheck = false;
    }).catch((err) => {
      this.confirmCheck = false;
    });
  }

}
